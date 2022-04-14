import React from 'react';
import TinyLabel from "./TinyLabel";

class ExperimentHeader extends React.Component {

    state = {
        path: null,
        title: null,
        description: null,
        notes: null,
        abfParents: [],
        abfDays: [],
    }

    componentDidMount() {
        this.loadProject(this.props.path);
    }

    loadProject(experimentFolderPath) {
        const url = `http://192.168.1.9/abf-browser/api/v4/experiment/?path=` + experimentFolderPath;
        console.log(url);
        fetch(url)
            .then(response => response.json())
            .then(json => {
                this.setState({
                    path: experimentFolderPath,
                    title: json.title,
                    description: json.description,
                    notes: json.notes,
                });
            });
    }

    render() {
        return <div className='' style={{ backgroundColor: "#CCC" }}>

            <TinyLabel text="EXPERIMENT" />

            <div className='ms-2 mb-2'>
                <div><b>{this.state.title}</b></div>
                <div>{this.state.description}</div>
            </div>

            <div className='pb-2'>
                <button class="btn btn-secondary btn-sm ms-2" type="button"
                    data-bs-toggle="collapse" data-bs-target="#collapseDetails2"
                    aria-expanded="false" aria-controls="collapseExample">
                    Groups (?)
                </button>
                <button class="btn btn-secondary btn-sm ms-2" type="button"
                    data-bs-toggle="collapse" data-bs-target="#collapseExperiments2"
                    aria-expanded="false" aria-controls="collapseExample">
                    Days (?)
                </button>
            </div>

        </div>
    }
}

export default ExperimentHeader;