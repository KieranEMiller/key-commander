import React from 'react';
import ReactDOM from 'react-dom';

class Content extends React.Component {
    render() {
        return (
            <React.Fragment>
                <div id="content_wrapper">
                    <div id="content" className="center">

                        <div className="clear_both"></div>
                    </div>
                </div>
                <div className="clear_both"></div>
            </React.Fragment>
        );
    }
}

export default Content;