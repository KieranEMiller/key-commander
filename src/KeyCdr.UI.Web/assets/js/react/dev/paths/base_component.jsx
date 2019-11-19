import React from 'react';
import ReactDOM from 'react-dom';

class BaseComponent extends React.Component {
    constructor(props) {
        super(props);
    }

    loadScript(src) {
        return new Promise(resolve => {
            var tag = document.createElement('script');
            tag.async = false;
            tag.src = src;
            tag.onload = () => { resolve(); };
            document.body.appendChild(tag);
        });
    }

    loadCss(src) {
        return new Promise(resolve => {
            var tag = document.createElement('link');
            tag.href = src;
            tag.type = "text/css";
            tag.rel = "stylesheet";
            tag.onload = () => { resolve(); };
            document.head.appendChild(tag);
        });
    }
}

export default BaseComponent;
