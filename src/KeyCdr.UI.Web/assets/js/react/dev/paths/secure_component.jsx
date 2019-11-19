import React from 'react';
import ReactDOM from 'react-dom';
import { Redirect, withRouter } from "react-router-dom";

import BaseComponent from './base_component.jsx';
import Loading from '../loading.jsx';
import ContentContainer from '../content.jsx';
import Auth from '../auth.jsx';

class SecureComponent extends BaseComponent {
    constructor(props) {
        super(props);

        console.log("secure route props ", props);
        this.auth = new Auth();
        this.state = {
            isAuthenticated: false,
            authFailed: null
        }

        var that = this;
        this.auth.isAuthenticated()
            .then(ret => {
                setTimeout(function () {
                    //notify the app
                    (ret == true) ? that.props.appLogin() : that.props.appLogout();
                    that.setState(() => ({
                        isAuthenticated: ret,
                        authFailed: !ret
                    }));
                }, 1000);
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
                    <Redirect to={{ pathname: '/login', state: { from: this.props.location.pathname } }} />
                )
            }
            else {
                return (
                    <ContentContainer>
                        <h2>Authenticating...</h2>
                        <div className="content_row">
                            <Loading showLoading={true}/>
                        </div>
                    </ContentContainer>
                )
            }
        }
    }
}

export default SecureComponent;