import React from 'react';
import ReactDOM from 'react-dom';

import BaseComponent from           './base_component.jsx';
import ContentContainer from        '../content.jsx';
import Auth from                    '../auth.jsx';
import Loading from                 '../components/loading.jsx';
import { Routes, UserMsgs } from    '../constants.jsx';

export default class Register extends BaseComponent {
    constructor(props) {
        super(props);

        this.state = {
            username: '',
            password: '',
            password_verify: '',
            showLoading: false,
            errors: [
                //error object format:
                //{msg: '', fieldid: '' }
            ]
        };

        this.AuthService = new Auth();
    }

    handleSubmit = (e) => {
        e.preventDefault();

        this.setState({ showLoading: true, errors: [] });

        var that = this;
        var errors = this.validate()
            .then(errors => {
                if (errors.length > 0) {
                    this.setState({ showLoading: false, errors: errors });
                }
                else {
                    this.AuthService.register(this.state.username, this.state.password)
                        .then(data => {
                            if (data.IsValid == true) {
                                that.props.history.push(Routes.MY_ACCT);
                            }
                            else {
                                errors.push({ fieldid: '', msg: 'authentication failure' });
                                that.setState({ showLoading: false, errors: errors });
                            }
                        })
                }
            })
    }

    showLoading = (showIt) => {
        this.setState({ showLoading: showIt });
    }

    handleChange = (e) => {
        this.setState(
            { [e.target.name]: e.target.value }
        );
        e.target.className = "";
    }

    validate = () => {
        var errors = [];
        if (this.state.username == "") {
            errors.push({ fieldid: 'username', msg: "Login name " + UserMsgs.Registration.MUST_BE_NON_EMPTY });
        }

        if (this.state.password == "") {
            errors.push({ fieldid: 'password1', msg: "Password " + UserMsgs.Registration.MUST_BE_NON_EMPTY });
        }

        if (this.state.password_verify == "") {
            errors.push({ fieldid: 'password2', msg: "Password verification " + UserMsgs.Registration.MUST_BE_NON_EMPTY });
        }
        
        if (this.state.password != this.state.password_verify)
            errors.push({ fieldid: 'password2', msg: UserMsgs.Registration.PASSWORD_MATCH });

        return this.AuthService.isUsernameInUse(this.state.username)
            .then(result => {
                if (result == true)
                    errors.push({ fieldid: 'username', msg: UserMsgs.Registration.USERNAME_IN_USE.replace("{0}", this.state.username) });

                return Promise.resolve(errors);
            })
    }

    render() {
        var errorMessages = this.state.errors.map((error, index) => {
            return (
                <li key={index} className='error'>{error.msg}</li>
            )
        });

        var errorFields = [];
        if (this.state.errors.length > 0) {
            errorFields = this.state.errors.reduce(function (result, item) {
                result.push(item.fieldid);
                return result;
            }, []);
        }

        return (
            <ContentContainer>
                <div className="center">
                    <h2>Register</h2>
                    <div className="content_row">
                        <Loading showLoading={this.state.showLoading}/>
                        <form onSubmit={this.handleSubmit} className="med_width">
                            <label htmlFor="username">Login Name: </label>
                            <input
                                id="username"
                                name="username"
                                type="text"
                                className={errorFields.indexOf("username") >= 0 ? "error_highlight":""}
                                onChange={this.handleChange} />

                            <label htmlFor="password">Password: </label>
                            <input
                                id="password1"
                                name="password"
                                type="password"
                                className={errorFields.indexOf("password1") >= 0 ? "error_highlight":""}
                                onChange={this.handleChange} />

                            <label htmlFor="password_verify">Re-enter Password: </label>
                            <input
                                id="password2"
                                name="password_verify"
                                type="password"
                                className={errorFields.indexOf("password2") >= 0 ? "error_highlight":""}
                                onChange={this.handleChange} />

                            {(this.state.errors.length > 0) &&
                                <ul className="error_list">
                                    {errorMessages}
                                </ul>
                            }

                            <input onClick={this.handleSubmit} className="button-size-medium" type="submit" value="Register" />
                            <div className="clear_both"></div>
                        </form>
                    </div>
                </div>
            </ContentContainer>
        );
    }
}
