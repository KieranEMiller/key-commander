import React from 'react';
import ReactDOM from 'react-dom';
import { Link, withRouter } from "react-router-dom";

import Auth from './auth.jsx'

const LOGOUT = { Path: "/Logout", Display: "Logout" };
const LOGIN = { Path: "/Login", Display: "Login" };

class HeaderNavBar extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            currentRoute: this.props.location.pathname,
            routes: [
                { route: "/Index", display: "Home", active: false },
                { route: "/secure/MyAccount", display: "My Account", active: false }
            ]
        };
    };

    clickHandler(route) {
        this.setState(prevState => {
            const list = prevState.routes.map((item, j) => {
                item.active = (item.route === route);
                return item;
            });

            return { list };
        });
    };

    render() {

        let link;
        if (this.props.IsAuthed === true) {
            link = <Link to='/Logout' onClick={this.props.appLogout}>Logout</Link>;
        } else {
            link = <Link to='/Login' onClick={() => this.clickHandler('/Login')}>Login</Link>;
        }

        let showSubMenu = (this.props.location.pathname == '/secure/MyAccount');

        return (
            <React.Fragment>
                <div id="nav_container" className="nav_container">
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
                {
                    /* submenu visible only for my account */
                    (showSubMenu === true) &&
                    <div id="sub_nav_container" className="nav_container_sm">
                        <ul>
                            <li>
                                <Link to='/NewSequence' onClick={() => this.clickHandler('/NewSequence')}>New Sequence</Link>
                            </li>
                            <li>
                                <Link to='/secure/History' onClick={() => this.clickHandler('/secure/History')}>History</Link>
                            </li>
                        </ul>
                    </div>
                }
                
            </React.Fragment>
        );
    }
}

export default withRouter(HeaderNavBar);