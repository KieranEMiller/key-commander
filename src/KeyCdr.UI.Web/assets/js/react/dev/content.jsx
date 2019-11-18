import React from 'react';
import ReactDOM from 'react-dom';

import Header from './header.jsx';

function ContentContainer(props) {
    return(
        <React.Fragment>
            <div id="content_wrapper">
                <div id="content" className="center">
                    {props.children}
                    <div className="clear_both"></div>
                </div>
            </div>
            <div className="clear_both"></div>
        </React.Fragment>
    );
}

export default ContentContainer;