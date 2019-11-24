import React from 'react';
import ReactDOM from 'react-dom';
import { Route, Link, BrowserRouter as Router, Switch } from 'react-router-dom'

import Routing from     './routing.jsx';
import Header from      './header.jsx';
import Footer from      './footer.jsx';
import Auth from        './auth.jsx';

import '../../../../assets/css/master.scss';

class App extends React.Component {
    constructor(props) {
        super(props);
        
        this.state = {
            IsAuthed: false
        }
    }

    appLogin = () => {
        this.setState({ IsAuthed: true });
    }

    appLogout = () => {
        var auth = new Auth();
        auth.logout();

        this.setState({ IsAuthed: false });
    }

    render() {
        return (
            <Router>
                <Header
                    IsAuthed={this.state.IsAuthed}
                    appLogin={this.appLogin}
                    appLogout={this.appLogout}
                />

                <Routing
                    IsAuthed={this.state.IsAuthed}
                    appLogin={this.appLogin}
                    appLogout={this.appLogout}
                />

                <Footer />
            </Router>
        ); 
    }
};

ReactDOM.render(
    <App/>,
    document.getElementById('app')
);
