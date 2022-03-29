import React from 'react';

class Counter extends React.Component {

    state = {
        count: 123
    };

    changeCount(delta) {
        this.setState({ count: this.state.count + delta });
    }

    setCount(newValue) {
        this.setState({ count: newValue });
    }

    render() {
        return (
            <div>
                Count: {this.state.count}
                <button onClick={() => this.changeCount(1)}>increment</button>
                <button onClick={() => this.setCount(0)}>reset</button>
                <button onClick={() => this.changeCount(-1)}>decrement</button>
            </div>
        );
    }
}

export default Counter;