import React from 'react';
import ReactDOM from 'react-dom';

import ContentContainer from '../content.jsx';
import Auth from '../auth.jsx';

class Index extends React.Component {
    navTo(path) {
        this.props.history.push(path);
    };
    render() {

        var auth = new Auth();

        var isLoggingOut = this.props.location.pathname === '/Logout';

        //logout uses the same view as the home page, just process the 
        //actual logout part before you render it
        if (isLoggingOut) {
            auth.logout();
        }

        return (
            <ContentContainer>
                <div className="content_row">
                    <h2>What is it?</h2>
                    <img src="/assets/img/main_banner.png" />
                    <ul className="prominent_list">
                        <li>Practice your typing against samples of text</li>
                        <li>Analyze your speed, accuracy and problem key combinations</li>
                        <li>Measure performance over time </li>
                    </ul>
 
                    <input className="button-size-medium" type="button" value="Try It!" />
                    <div className="clear_both"></div>
                </div>

                <div className="content_row">
                    <h2>Already Have an Account?</h2>
                    Login and continue where you left off
                    <input className="button-size-medium" type="button"
                        value="Login to Your Account"
                        onClick={() => this.navTo('/Login')}
                    />
                </div>

                <div className="content_row">
                    <h2>Review Reporting Features</h2>
                    Review a public account to see the analytics on speed and accuracy
                    <input className="button-size-medium" type="button" value="Review Reporting" />
                </div>
            </ContentContainer>
        );
    }
}

export default Index;