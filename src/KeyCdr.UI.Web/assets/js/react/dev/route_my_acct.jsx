import React from 'react';
import ReactDOM from 'react-dom';
import { Redirect} from 'react-router-dom'

import ContentContainer from './content.jsx';
import SecureRouteComponent from './secure_route_component.jsx';

class MyAccount extends SecureRouteComponent {
    constructor(props) {
        super(props);

    }
    render() {
        if (this.authenticationComplete()) {
            return (
                <ContentContainer authed={true}>
                    <div className="content_row">
                        <h2>My Account</h2>
                        this should be a private only accessible page once the user has logged in
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