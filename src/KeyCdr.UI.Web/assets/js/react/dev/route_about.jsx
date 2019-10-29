import React from 'react';
import ReactDOM from 'react-dom';

import ContentContainer from './content.jsx';

class About extends React.Component {
    render() {
        return (
            <ContentContainer>
                <h2>About Key Commander</h2>
                <div className="clear_both"></div>
            </ContentContainer>
        );
    }
}

export default About;