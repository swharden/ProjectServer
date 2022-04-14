import React from 'react';

class TinyLabel extends React.Component {
    render() {
        const styleDarkText = { backgroundColor: "#00000011", color: '#000000AA' };
        const styleLightText = { backgroundColor: "#00000022", color: '#FFFFFF66' };
        const style = this.props.light ? styleLightText : styleDarkText;

        return (
            <div className='' style={{ fontSize: '.6em' }}>
                <div className='d-inline-block px-1' style={style}>
                    {this.props.text}
                </div>
            </div>
        );
    }
}

export default TinyLabel;
