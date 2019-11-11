import React from 'react';
import ReactDOM from 'react-dom';
import { Redirect } from 'react-router-dom'

import Loading from './loading.jsx';
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
                setTimeout(function () {
                    that.setState(() => ({
                        isAuthenticated: ret,
                        authFailed: !ret
                    }));
                }, 1000);
            });
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

export default SecureRouteComponent;