import React from 'react';
import ReactDOM from 'react-dom';
import { Link, withRouter } from "react-router-dom";

import Auth from './auth.jsx'

const LOGOUT = { Path: "/Logout", Display: "Logout" };
const LOGIN = { Path: "/Login", Display: "Login" };

//the boolean key represents the current logged in status
//ex: if true, it means the user is logged in so show logout
const loginLogoutToggle = {
    false: { route: LOGIN.Path, display: LOGIN.Display, active: false},
    true: { route: LOGOUT.Path, display: LOGOUT.Display, active: true }
};

class HeaderNavBar extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            currentRoute: this.props.location.pathname,
            routes: [
                { route: "/Index", display: "Home", active: false },
                { route: "/secure/MyAccount", display: "My Account", active: false },
                { route: "/About", display: "About", active: false }
            ]
        };
    };

    clickHandler(route) {
        this.setState(prevState => {
            const list = prevState.routes.map((item, j) => {
                item.active = (item.route === route);

                //only if the user is logging out do we want to switch up 
                //the header text immediately.  if the user clicks the login
                //link they are not immediately logged in, so don't switch
                //if (route === LOGOUT.Path) {
                //    item = loginLogoutToggle[false];
                //}

                return item;
            });

            return { list };
        });
    };

    updateAuthState(authResult) {
        this.setState(prevState => {
            const list = prevState.routes.map((item, j) => {
                if (item.Route === LOGOUT.Path || item.Route === LOGIN.Path) {
                    item = loginLogoutToggle[authResult];
                }
            });
            return { list };
        });
    };

    render() {

        const isLoggedIn = this.props.IsAuthed;
        let link;

        if (isLoggedIn) {
            link = <Link to='/Logout' onClick={this.props.appLogout}>Logout</Link>;
        } else {
            link = <Link to='/Login' onClick={() => this.clickHandler('/Login')}>Login</Link>;
        }

        return (
            <React.Fragment>
                <div id="nav_container">
                    <ul>
                        {this.state.routes.map((route, index) => {
                            var css = (route.active) ? 'current' : '';
                            return (
                                <li key={index} className={css}>
                                    <Link to={route.route} onClick={() => this.clickHandler(route.route)}>{route.display}</Link>
                                </li>
                            )   
                        })}
                        <li>{link}</li>
                    </ul>
                </div>
            </React.Fragment>
        );
    }
}

export default withRouter(HeaderNavBar);