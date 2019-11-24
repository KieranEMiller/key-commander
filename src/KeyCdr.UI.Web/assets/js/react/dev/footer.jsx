import React from 'react';
import ReactDOM from 'react-dom';

export default class Footer extends React.Component {
    constructor(props) {
        super(props);
    };
    render() {
        return (
            <React.Fragment>
                <div id="footer">
                    <div id="footer_contents">
                        <span className="float_left">UTC: {new Date().toUTCString()}</span>
                        <span className="float_right">
                            &copy; <a href="http://kieranemiller.com">kieranemiller.com</a>
                        </span>
                        <div className="clear_both"></div>
                    </div>

                    <div className="top_bar"></div>
                    <div className="clear_both"></div>
                </div>
            </React.Fragment>
        );
    }
}
