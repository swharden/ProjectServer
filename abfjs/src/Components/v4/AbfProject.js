import React from 'react';

import ProjectHeader from "./ProjectHeader";
import ExperimentHeader from "./ExperimentHeader";
import ExperimentSummary from "./ExperimentSummary";
import TinyLabel from "./TinyLabel";

class AbfProject extends React.Component {

    render() {
        return (
            <>
                <ProjectHeader path={"X:/Projects/Aging-eCB"} />
                <ExperimentHeader path={"X:/Projects/Sigma1R/experiments/METH intrinsic properties"} />
                <ExperimentSummary />
            </>
        );
    }
}

export default AbfProject;
