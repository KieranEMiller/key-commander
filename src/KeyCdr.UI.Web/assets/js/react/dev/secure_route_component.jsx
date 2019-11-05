import React from 'react';
import ReactDOM from 'react-dom';
import { Redirect } from 'react-router-dom'

import ContentContainer from './content.jsx';
import Auth from './auth.jsx';

class SecureRouteComponent extends React.Component {
    constructor(props) {
        super(props);

        this.auth = new Auth();
        this.state = {
            isAuthenticated: false,
            authFailed: undefined
        }

        var that = this;
        this.auth.isAuthenticated()
            .then(ret => {
                that.setState(() => ({
                    isAuthenticated: ret,
                    authFailed: !ret
                }));
            });
    }

    authenticationComplete() {
        if (this.state.isAuthenticated === true
            && this.state.authFailed === false)
            return true;

        return false;
    }

    render() {
        if (!this.state.isAuthenticated) {
            if (this.state.authFailed) {
                return (
                    <Redirect to={{ pathname: '/login', state: { from: '/secure/MyAccount' } }} />
                )
            }
            else {
                return (
                    <ContentContainer>
                    <div className="content_row">
                        <h2>Authenticating...</h2>
                    </div>
                    </ContentContainer>
                )
            }
        }
    }
}

export default SecureRouteComponent;