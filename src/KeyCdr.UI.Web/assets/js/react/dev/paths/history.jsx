import React from 'react';
import ReactDOM from 'react-dom';
import { Redirect} from 'react-router-dom'

import ContentContainer from '../content.jsx';
import SecureRouteComponent from '../secure_route_component.jsx';

class History extends SecureRouteComponent {
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
                    <h2>Session History</h2>

                    <div className="spinner-border" role="status">
                        <span className="sr-only">Loading...</span>
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

export default History;