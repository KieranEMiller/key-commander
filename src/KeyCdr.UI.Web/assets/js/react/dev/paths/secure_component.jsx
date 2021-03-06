﻿import React from 'react';
import ReactDOM from 'react-dom';
import { Redirect, withRouter } from "react-router-dom";

import BaseComponent from       './base_component.jsx';
import Loading from             '../components/loading.jsx';
import ContentContainer from    '../content.jsx';
import Auth from                '../auth.jsx';

class SecureComponent extends BaseComponent {
    constructor(props) {
        super(props);

        this.auth = new Auth();
        this.state = {
            isAuthenticated: false,
            authFailed: null
        }

        var that = this;
        this.auth.isAuthenticated()
            .then(ret => {
                if (ret == true)
                    that.props.appLogin();
                else
                    that.props.appLogout();

                that.setState({
                    isAuthenticated: ret,
                    authFailed: !ret
                });
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