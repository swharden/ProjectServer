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
                <ExperimentHeader path={"X:/Projects/Aging-eCB/abfs/exp1 - DSI in CA1"} />
                <ExperimentSummary />
                <footer className='mt-5'>
                    <TinyLabel text="FOOTER" />
                    <div className='p-1'>
                        <a href='http://192.168.1.9/abf-browser/api/v4/' style={{ textDecoration: 'none' }}>API Version 4</a>
                    </div>
                </footer>
            </>
        );
    }
}

export default AbfProject;
