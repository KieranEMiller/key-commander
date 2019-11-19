import React from 'react';
import ReactDOM from 'react-dom';

import BaseComponent from './base_component.jsx';
import ContentContainer from '../content.jsx';
import Loading from '../loading.jsx';
import HideableScreenMask from '../components/hideable_screen_mask.jsx';
import ToolbarNewSequence from '../components/toolbar_new_sequence.jsx';

const ERROR_COUNTER_TEXT = [
    "PRESS ENTER TWICE TO BEGIN (2 REMAINING)",
    "PRESS ENTER TWICE TO BEGIN (1 REMAINING)...GET READY"
];

class NewSequence extends BaseComponent {
    constructor(props) {
        super(props);

        this.state = {
            EnterCounter: 0,
            TextShown: ERROR_COUNTER_TEXT[0],
            IsRunning: false,
            IsLoading: false,
            UseTheatreMode: false,
            CurrentSequence: null
        }
    }

    toggleTheatreMode = () => {
        this.setState({ UseTheatreMode: !this.state.UseTheatreMode });
    }

    componentDidMount() {
        this.inputSequenceField.focus();
    }

    inputKeyDown = (e) => {
        if (e.keyCode == 13) {
            e.preventDefault();
            this.handleKeyEnter();
        }
    }

    handleKeyEnter() {
        if (this.state.IsRunning == true) {
            this.stopSequence();
        }
        else if (this.state.EnterCounter == 1) {
            this.startSequence();
        }
        else if (this.state.EnterCounter == 0) {
            this.setState({
                EnterCounter: 1,
                TextShown: ERROR_COUNTER_TEXT[1]
            });
        }
    }

    startSequence() {
        this.setState({ IsLoading: true });

        fetch('/api/Sequence/GetNewSequence', {
            method: "Get",
            headers: { 'Content-Type': 'application/json', Accept: 'application/json' }
        })
        .then(resp => resp.json())
        .then(data => {
            console.log("new seq data", data);

            this.setState({
                CurrentSequence: data,
                EnterCounter: 0,
                TextShown: data.Text,
                IsRunning: true,
                IsLoading: false,
                UseTheatreMode: true
            });
        });
    }

    stopSequence = () => {
        this.setState({
            IsRunning: false,
            UseTheatreMode: false
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
                <h2>New Sequence</h2>
                
                <div className="content_row_sm show_above_mask">
                    <Loading showLoading={this.state.IsLoading} />
                    <h3>Text to Type</h3>
                        
                    <form>
                        <textarea name="textShown"
                            value={this.state.TextShown}
                            readOnly
                        />
                    </form>
                </div>

                <div className="content_row_sm show_above_mask">
                    <h3>Your Text</h3>

                    <form>
                        <textarea name="textEntered"
                                ref={(input) => { this.inputSequenceField = input; }} 
                                onKeyDown={this.inputKeyDown}
                        />
                    </form>
                </div>
                </ContentContainer>
                </React.Fragment>
        );
    }
}

export default NewSequence;