import React from 'react';

import ProjectHeader from "./ProjectHeader";
import ExperimentHeader from "./ExperimentHeader";
import ExperimentSummary from "./ExperimentSummary";

class AbfProject extends React.Component {

    render() {
        return (
            <>
                <ProjectHeader path={"X:/Projects/Aging-eCB"} />
                <ExperimentHeader />
                <ExperimentSummary />
            </>
        );
    }
}

export default AbfProject;
