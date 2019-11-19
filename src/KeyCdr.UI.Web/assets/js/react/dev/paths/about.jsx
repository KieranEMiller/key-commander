import React from 'react';
import ReactDOM from 'react-dom';

import BaseComponent from './base_component.jsx';
import ContentContainer from '../content.jsx';

class About extends BaseComponent {
    render() {
        return (
            <ContentContainer>
                <div className="content_row">
                    <h2>About Key Commander</h2>
                    Key Commander is an application that you can use to:
                    <ul>
                        <li>Practice your typing against samples of text</li>
                        <li>Analyze your speed, accuracy and problem key combinations</li>
                        <li>Measure performance over time </li>
                    </ul>
                </div>
            </ContentContainer>
        );
    }
}

export default About;