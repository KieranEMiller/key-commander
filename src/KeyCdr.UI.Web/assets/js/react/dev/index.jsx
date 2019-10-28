import ReactDOM from 'react-dom'
import React from 'react'

import Header from './header.jsx';

class App extends React.Component {
    render() {
        return (
            <React.Fragment>
                <Header />
                <div id="content_wrapper">
                    <div id="content" className="center">
                        <div className="content_row">
                            <h2>What is it?</h2>
                            <img src="/assets/img/main_banner.png" />
                            <ul>
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
                            <input className="button-size-medium" type="button" value="Login to Your Account" />
                        </div>

                        <div className="content_row">
                        <h2>Review Reporting Features</h2>
                            Review a public account to see the analytics on speed and accuracy
                            <input className="button-size-medium" type="button" value="Review Reporting" />
                        </div>

                        <div className="clear_both"></div>
                    </div>

                </div>
                <div className="clear_both"></div>
            </React.Fragment>
        );
    }
}

ReactDOM.render(
    <App />,
    document.getElementById('app')
);
