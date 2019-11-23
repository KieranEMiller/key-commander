import React from 'react';
import ReactDOM from 'react-dom';

class DeltaIndicator extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            Value: props.Val1,
            ValueToDisplay: (props.ValToDisplay) ? props.ValToDisplay : null,
            Delta: Math.round((props.Val1 - props.Val2) * 100) / 100,
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
                    {(this.state.ValueToDisplay != null)
                        ? this.state.ValueToDisplay
                        : this.state.Value
                    }
                </span>

                <span className="delta_change_indicator">
                {(this.state.Delta != 0) &&
                     arrow 
                }

                {this.state.Delta}
                </span>
            </span>
        );
    }
}

export default DeltaIndicator;