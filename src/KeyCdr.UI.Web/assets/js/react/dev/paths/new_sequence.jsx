import React from 'react';
import ReactDOM from 'react-dom';

import BaseComponent from './base_component.jsx';
import ContentContainer from '../content.jsx';
import Loading from '../loading.jsx';
import HideableScreenMask from '../components/hideable_screen_mask.jsx';
import ToolbarNewSequence from '../components/toolbar_new_sequence.jsx';

const INSTRUCTIONS = "PRESS SHIFT+ENTER OR PRESS START TO BEGIN";
const DEFAULT_INPUT_SEQ_HEIGHT = 75;

class NewSequence extends BaseComponent {
    constructor(props) {
        super(props);

        this.state = {
            TextShown: INSTRUCTIONS,
            IsRunning: false,
            IsLoading: false,
            IsFinalizingSequence: false,
            UseTheatreMode: false,
            CurrentSequence: null,
            height: DEFAULT_INPUT_SEQ_HEIGHT
        }
    }

    toggleTheatreMode = () => {
        this.setState({ UseTheatreMode: !this.state.UseTheatreMode });
    }

    componentDidMount() {
        this.inputSequenceField.focus();
    }

    autoresize() {
        this.setFilledTextareaHeight();
    }

    inputKeyDown = (e) => {
        if (e.shiftKey == true && e.keyCode == 13) {
            e.preventDefault();
            this.handleKeyEnter();
        }
        else if (e.keyCode == 13) {
            e.preventDefault();
        }
    }

    handleKeyEnter() {
        if (this.state.IsRunning == true) {
            this.stopSequence();
        }
        else {
            this.startSequence();
        }
    }

    startSequence() {
        /* abort start request if its already loading or running */
        if (this.state.IsLoading == true
            || this.state.IsRunning == true)
            return;

        this.setState({ IsLoading: true });

        fetch('/api/Sequence/GetNewSequence', {
            method: "Get",
            headers: { 'Content-Type': 'application/json', Accept: 'application/json' }
        })
        .then(resp => resp.json())
        .then(data => {
            this.setState({
                CurrentSequence: data,
                TextShown: data.Text,
                IsRunning: true,
                IsLoading: false,
                UseTheatreMode: true
            });
            this.setFilledTextareaHeight();
        });
    }

    stopSequence = () => {
        this.setState({
            IsRunning: false,
            UseTheatreMode: false,
            IsFinalizingSequence: true
        });
    }

    setFilledTextareaHeight() {
        this.setState({
            height: this.sizeref.clientHeight + 10,
        });
    }

    render() {
        return (
            <React.Fragment>
                <HideableScreenMask MaskIsVisible={this.state.UseTheatreMode} />

                <ToolbarNewSequence
                    MaskIsVisible={this.state.UseTheatreMode}
                    IsRunning={this.state.IsRunning}
                    onToggleMask={this.toggleTheatreMode}
                    onStopSequence={this.stopSequence}
                />

                <ContentContainer>
                    <Loading showLoading={this.state.IsFinalizingSequence} />
                    <h2>New Sequence</h2>
                
                    <div className="content_row_sm show_above_mask">
                        <Loading showLoading={this.state.IsLoading} />
                        <h3>Text to Type</h3>
                        
                        <form>
                            <textarea name="textShown"
                                className="presented_text"
                                value={this.state.TextShown}
                                style={{
                                    height: this.state.height,
                                    resize: this.state.height <= DEFAULT_INPUT_SEQ_HEIGHT ? "none" : null
                                }}
                                readOnly
                            />
                        </form>
                    </div>

                    <div className="content_row_sm show_above_mask">
                        <h3>Your Text</h3>

                        <form>
                            <textarea name="textEntered"
                                className="entered_text" 
                                ref={(input) => { this.inputSequenceField = input; }} 
                                onKeyDown={this.inputKeyDown}
                                style={{
                                    height: this.state.height,
                                    resize: this.state.height <= DEFAULT_INPUT_SEQ_HEIGHT ? "none" : null
                                }}
                            />

                            {(this.state.IsRunning === false
                                && this.state.IsLoading === false
                                && this.state.IsFinalizingSequence === false) &&
                                <input onClick={() => this.startSequence()} className="button-size-medium position_center" type="button" value="Start Sequence...!" />
                            }
                        </form>
                    </div>

                    <div className="content_row_sm">
                        <form>
                            <div id="sizeComparison" className="presented_text_size_ref" ref={(c) => this.sizeref = c}>
                                {this.state.TextShown}
                        </div>
                        </form>
                    </div>
                </ContentContainer>
                </React.Fragment>
        );
    }
}

export default NewSequence;