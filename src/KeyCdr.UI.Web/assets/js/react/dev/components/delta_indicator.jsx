import React from 'react';
import ReactDOM from 'react-dom';

class DeltaIndicator extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            Value: props.Val1,
            Delta: Math.round((props.Val1 - props.Val2) * 100) / 100
        }
    }

    render() {
        let arrow;
        
        if (this.state.Delta > 0)
            arrow = <img src="/assets/img/triangle_up.png" alt="increase" />
        else if (this.state.Delta < 0)
            arrow = <img src="/assets/img/triangle_down.png" alt="decrease" />
        
        return (
            <span className="delta">
                <span className="delta_label">
                    {this.state.Value}
                </span>

                {(this.state.Delta != 0) &&
                     arrow 
                }

                {this.state.Delta}
            </span>
        );
    }
}

export default DeltaIndicator;