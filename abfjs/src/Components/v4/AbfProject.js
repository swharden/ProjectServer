import React from 'react';

import ProjectHeader from "./ProjectHeader";
import ExperimentHeader from "./ExperimentHeader";
import ExperimentSummary from "./ExperimentSummary";

class AbfProject extends React.Component {

    render() {
        return (
            <>
                <ProjectHeader path={"X:/Projects/Aging-eCB"} />
                <ExperimentHeader path={"X:/Projects/Aging-eCB/abfs/exp1 - DSI in CA1"} />
                <ExperimentSummary />
            </>
        );
    }
}

export default AbfProject;
