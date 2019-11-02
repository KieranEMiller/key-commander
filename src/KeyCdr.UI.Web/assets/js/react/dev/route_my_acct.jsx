import React from 'react';
import ReactDOM from 'react-dom';

import Header from      './header.jsx';
import ContentContainer from './content.jsx';

class MyAccount extends React.Component {
    render() {
        return (
            <ContentContainer>
                <div className="content_row">
                    <h2>My Account</h2>
                    this should be a private only accessible page once the user has logged in
                </div>
            </ContentContainer>
        );
    }
}

export default MyAccount;