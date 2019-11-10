import React from 'react';
import ReactDOM from 'react-dom';

import ContentContainer from '../content.jsx';
import Auth from '../auth.jsx';
import Loading from '../loading.jsx';

class Login extends React.Component {
    constructor(params) {
        super(params);
        this.state = {
            username: '',
            password: '',
            showLoading: false,
            errorMsg: ''
        };
        this.AuthService = new Auth();
    }

    handleSubmit = (e) => {
        var logindata = { username: this.state.username, password: this.state.password }; 

        this.setState({ errorMsg: '' });
        this.showLoading(true);

        var that = this;
        this.AuthService.login(logindata)
            .then(data => {
                setTimeout(function () {
                    that.showLoading(false);

                    if (data.isvalid)
                        that.props.history.push('/secure/MyAccount');
                    else
                        that.setState({ errorMsg: 'unable to login with the username and password provided' });

                }, 1000);
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

                            {(this.state.errorMsg == '')
                                ? ''
                                : <p className="form_error">{this.state.errorMsg}</p>
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