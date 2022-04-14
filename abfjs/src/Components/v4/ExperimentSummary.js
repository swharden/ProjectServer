import React from 'react';
import TinyLabel from "./TinyLabel";

class ExperimentSummary extends React.Component {

    render() {
        return <div>
            <TinyLabel text="EXPERIMENT" />
            <div className='p-1'>
                <div>Experiment Summary</div>
            </div>
        </div>
    }
}

export default ExperimentSummary;