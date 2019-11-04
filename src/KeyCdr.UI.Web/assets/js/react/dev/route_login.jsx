import React from 'react';
import ReactDOM from 'react-dom';

import ContentContainer from './content.jsx';
import Auth from './auth.jsx';

class Login extends React.Component {
    constructor(params) {
        super(params);
        this.state = {
            username: '',
            password: ''
        };
        this.AuthService = new Auth();
    }

    handleSubmit = (e) => {
        var logindata = { username: this.state.username, password: this.state.password }; 
        console.log("submitting", logindata);

        this.AuthService.login(logindata)
            .then(data => {
                if (data.isvalid)
                    this.props.history.push('/secure/MyAccount');
                else
                    alert("unable to login");
            })
            .catch(err => {
                console.log(err);
            });

        e.preventDefault();
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

                        <input onClick={this.handleSubmit} className="button-size-medium" type="submit" value="Login" />
                    </form>
                </div>
            </ContentContainer>
        );
    }
}

export default Login;