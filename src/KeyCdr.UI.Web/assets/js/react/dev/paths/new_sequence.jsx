import React from 'react';
import ReactDOM from 'react-dom';

import SecureComponent from          './secure_component.jsx';
import ContentContainer from         '../content.jsx';
import Loading from                  '../components/loading.jsx';
import HideableScreenMask from       '../components/hideable_screen_mask.jsx';
import ToolbarNewSequence from       '../components/toolbar_new_sequence.jsx';
import SessionManager from           '../session_mgr.jsx';
import KeySequenceErrorDisplay from  '../components/key_sequence_error_display.jsx';
import { Routes, Urls, UserMsgs, Runtime } from  '../constants.jsx';

class NewSequence extends SecureComponent {
    constructor(props) {
        super(props);

        this.state = {
            TextShown: UserMsgs.NEW_SEQUENCE_INSTRUCTIONS,
            TextEntered: "",
            IsRunning: false,
            IsLoading: false,
            UseTheatreMode: false,
            CurrentSequence: { SequenceId: -1 },
            height: Runtime.DEFAULT_NEW_SEQUENCE_INPUT_FIELD_HEIGHT
        };

        this.SessionManager = new SessionManager();
    }

    toggleTheatreMode = () => {
        this.setState({ UseTheatreMode: !this.state.UseTheatreMode });
    }

    componentDidMount() {
        if(this.inputSequenceField)
            this.inputSequenceField.focus();

        document.querySelector('body').addEventListener('keydown', (e) => {
            this.onTextEnteredKeyDown(e);
        });
    }

    autoresize() {
        this.setFilledTextareaHeight();
    }

    onTextEnteredChanged = (e) => {
        this.setState({ TextEntered: e.target.value })
    }

    onTextEnteredKeyDown = (e) => {
        if (e.shiftKey == true && e.keyCode == 13) {
            e.preventDefault();
            this.handleKeyEnter();
        }
        else if (e.keyCode == 13) {
            e.preventDefault();
        }
    }

    handleKeyEnter = () => {
        if (this.state.IsRunning == true) {
            this.stopSequence();
        }
        else {
            this.startSequence();
        }
    }

    startSequence = () => {
        /* abort start request if its already loading or running */
        if (this.state.IsLoading == true
            || this.state.IsRunning == true)
            return;

        this.setState({ IsLoading: true });
        this.SessionManager.getNewSequence()
            .then(result => {

                /*TODO if result is undefined here, there was a
                problem and need to handle it accordingly */
                this.setState({
                    CurrentSequence: result,
                    TextShown: result.Text,
                    IsRunning: true,
                    IsLoading: false,
                    UseTheatreMode: true
                });

                this.setFilledTextareaHeight();
                this.SessionManager.sequenceStarting();
            });
    }

    stopSequence = () => {
        this.setState({
            IsRunning: false,
            UseTheatreMode: false,
            IsLoading: true
        });

        this.SessionManager.stopCurrentSequence(this.state.TextEntered)
            .then(data => {
                this.props.history.push(Routes.HISTORY_DETAILS + "/" + data.SequenceId);
            })
    }

    setFilledTextareaHeight() {
        this.setState({
            height: this.sizeref.clientHeight + 10,
        });
    }

    render() {
        if (this.authenticationComplete()) {
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

                    <div className="position_relative">
                        <Loading showLoading={this.state.IsLoading} position="top" /> 

                        <div className="content_row_sm show_above_mask">
                            <h3>Text to Type</h3>

                            <KeySequenceErrorDisplay
                                TextShown={this.state.TextShown}
                                TextEntered={this.state.TextEntered}
                                sequenceId={this.state.CurrentSequence.SequenceId}
                                peekInRealTime={true}
                                IsRunning={this.state.IsRunning}
                            />

                            {/*
                            <form>
                                <textarea name="textShown"
                                    className="presented_text"
                                    value={this.state.TextShown}
                                    style={{
                                        height: this.state.height,
                                        resize: this.state.height <= Runtime.DEFAULT_NEW_SEQUENCE_INPUT_FIELD_HEIGHT ? "none" : null
                                    }}
                                    readOnly
                                />
                            </form>
                            */}
                        </div>

                        <div className="content_row_sm show_above_mask">
                            <h3>Your Text</h3>

                            <form>
                                <textarea name="textEntered"
                                    className="entered_text" 
                                    ref={(input) => { this.inputSequenceField = input; }} 
                                    onKeyDown={this.onTextEnteredKeyDown}
                                    onChange={this.onTextEnteredChanged}
                                    value={this.state.TextEntered} 
                                    style={{
                                        height: this.state.height,
                                        resize: this.state.height <= Runtime.DEFAULT_NEW_SEQUENCE_INPUT_FIELD_HEIGHT ? "none" : null
                                    }}
                                />

                                {(this.state.IsRunning === false
                                    && this.state.IsLoading === false) &&
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

                    </div>
                </ContentContainer>
                </React.Fragment>
            );
        }
        else {
            return (
                super.render()
            )
        }
    }
}

export default NewSequence;