import React from 'react';
import ReactDOM from 'react-dom';

import ContentContainer from './content.jsx';

class Login extends React.Component {
    constructor(params) {
        super(params);
        this.state = {
            username: '',
            password: ''
        };
    }
    handleSubmit = (e) => {
        console.log(this.state);
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

                        <input className="button-size-medium" type="submit" value="Login" />
                    </form>
                </div>
            </ContentContainer>
        );
    }
}

export default Login;