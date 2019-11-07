import React from 'react';
import ReactDOM from 'react-dom';

class Loading extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            showLoading: true
        }
    }

    render() {
        if (this.state.showLoading) {
            return (
                <React.Fragment>
                    <div className="loading_mask">
                        <div className="spinner-border"></div>
                    </div>
                </React.Fragment>
            )
        }
    }
}

export default Loading;