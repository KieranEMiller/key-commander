import React from 'react';
import ReactDOM from 'react-dom';
import { Redirect} from 'react-router-dom'

import ContentContainer from    '../content.jsx';
import Loading from             '../components/loading.jsx';
import SecureComponent from     './secure_component.jsx';
import { Routes } from          '../constants.jsx';

class MyAccount extends SecureComponent {
    constructor(props) {
        super(props);
    }

    navTo(path) {
        this.props.history.push(path);
    }

    render() {
        if (this.authenticationComplete()) {
            return (
                <ContentContainer authed={true}>
                    <h2>My Account</h2>

                    <div className="content_row_sm block_hover_highlight" onClick={()=>this.navTo(Routes.NEW_SEQUENCE)} >
                        <h3>Start a new Session</h3>
                        <img className="thumb" src="/assets/img/new_session_thumb.png" />

                        <ul className="">
                            <li>Start a new typing session</li>
                            <li>Configure your sample source </li>
                        </ul>

                        <input onClick={() => this.navTo(Routes.NEW_SEQUENCE)} className="button-size-medium position_bottom_right" type="button" value="Start New Session" />
                        <div className="clear_both"></div>
                    </div>

                    <div className="content_row_sm block_hover_highlight" onClick={() => this.navTo(Routes.HISTORY)} >
                        <h3>Your Session History</h3>
                        <img className="thumb" src="/assets/img/graph_thumbnail.png" />
                        <ul className="">
                            <li>Practice your typing against samples of text</li>
                            <li>Analyze your speed, accuracy and problem key combinations</li>
                            <li>Measure performance over time </li>
                        </ul>

                        <input onClick={() => this.navTo(Routes.HISTORY)} className="button-size-medium position_bottom_right" type="button" value="View Session History" />
                        <div className="clear_both"></div>
                    </div>

                </ContentContainer>
            );
        }
        else {
            return (
                super.render()
            )
        }
    }
}

export default MyAccount;