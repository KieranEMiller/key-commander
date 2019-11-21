import React from 'react';
import ReactDOM from 'react-dom';

class Loading extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        if (this.props.showLoading) {
            var css = (this.props.position === 'top') ? "spinner-border-position-top" : "spinner-border";
            return (
                <React.Fragment>
                    <div className="loading_mask">
                        <div className={css}></div>
                    </div>
                </React.Fragment>
            )
        }
        else {
            return (<React.Fragment></React.Fragment>)
        }
    }
}

export default Loading;