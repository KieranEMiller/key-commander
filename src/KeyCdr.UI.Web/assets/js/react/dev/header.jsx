import React from 'react';
import ReactDOM from 'react-dom';

class Header extends React.Component {
    render() {
        return (
            <React.Fragment>
                <div id="header">
                    <div className="top_bar"></div>
                    <div id="header_contents">
                        <div id="title_block">
                            <a href="test.com" className="title">Key Commander</a>
                            <div className="title_sub">measure, improve and analyze typing speed and accuracy</div>
                        </div>
                        <div id="nav_container">
                            <ul>
                                <li className="current">
                                    <a href="/Index">Home</a>
                                </li>
                                <li>
                                    <a href="#">My Account</a>
                                </li>
                                <li>
                                    <a href="/About">About</a>
                                </li>
                            </ul>
                        </div>

                        <div className="clear_both"></div>
                    </div>

                    <div className="clear_both"></div>
                </div>
            </React.Fragment>
        );
    }
}

export default Header;
