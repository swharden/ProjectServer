import React from 'react';

class ExperimentHeader extends React.Component {

    render() {
        return <div className='p-1' style={{ backgroundColor: "#CCC" }}>
            <div>Experiment Title</div>
            <div>Experiment Description</div>
        </div>
    }
}

export default ExperimentHeader;