import React from 'react';
import ReactDOM from 'react-dom';

import BaseComponent from           './base_component.jsx';
import ContentContainer from        '../content.jsx';
import Auth from                    '../auth.jsx';
import Loading from                 '../components/loading.jsx';
import { Routes, UserMsgs } from    '../constants.jsx';

class Login extends BaseComponent {
    constructor(props) {
        super(props);

        this.state = {
            username: '',
            password: '',
            showLoading: false,
            errorMsg: '',
            redirectToOnLogin: this.getPathToRedirectAfterLogin()
        };

        this.AuthService = new Auth();
    }

    getPathToRedirectAfterLogin() {
        var path = Routes.MY_ACCT;
        if (this.props &&
            this.props.location &&
            this.props.location.state &&
            this.props.location.state.from) {
                path = this.props.location.state.from;
        }
        return path;
    }

    handleSubmit = (e) => {
        var logindata = { username: this.state.username, password: this.state.password }; 

        this.setState({ errorMsg: '' });
        this.showLoading(true);

        var that = this;
        this.AuthService.login(logindata)
            .then(data => {
                that.showLoading(false);

                if (data.IsValid) {
                    that.props.appLogin();
                    that.props.history.push(that.state.redirectToOnLogin);
                }
                else
                    that.setState({ errorMsg: UserMsgs.AUTH_FAILURE });
            })
            .catch(err => {
                console.log(err);
            });
            
        e.preventDefault();
    }

    showLoading = (showIt) => {
        this.setState({ showLoading: showIt });
    }

    handleChange = (e) => {
        this.setState(
            { [e.target.name]: e.target.value }
        );
    }

    render() {

        return (
            <ContentContainer>
                <div className="center">
                    <h2>Login</h2>
                    <div className="content_row">
                        <Loading showLoading={this.state.showLoading}/>
                        <form onSubmit={this.handleSubmit} className="med_width">
                            <label htmlFor="username">User Name: </label>
                            <input
                                name="username"
                                type="text"
                                onChange={this.handleChange} />

                            <label htmlFor="password">Password: </label>
                            <input
                                name="password"
                                type="password"
                                onChange={this.handleChange} />

                            {(this.state.errorMsg != '') &&
                                <p className="form_error">{this.state.errorMsg}</p>
                            }

                            <input onClick={this.handleSubmit} className="button-size-medium" type="submit" value="Login" />
                            <div className="clear_both"></div>
                        </form>
                    </div>
                </div>
            </ContentContainer>
        );
    }
}

export default Login;