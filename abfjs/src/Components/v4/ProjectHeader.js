import React from 'react';

class ProjectHeader extends React.Component {

    state = {
        path: null,
        title: null,
        description: null,
        notes: null,
        experiments: [],
    }

    componentDidMount() {
        this.loadProject(this.props.path);
    }

    loadProject(projectFolderPath) {
        const url = `http://192.168.1.9/abf-browser/api/v4/project/?path=` + projectFolderPath;
        console.log(url);
        fetch(url)
            .then(response => response.json())
            .then(json => {
                this.setState({
                    path: projectFolderPath,
                    title: json.title,
                    description: json.description,
                    notes: json.notes,
                    experiments: json.abfExperiments,
                });
            });
    }

    render() {
        return <div className='text-light' style={{ backgroundColor: "#003366" }}>

            <div className='' style={{ fontSize: '.6em' }}>
                <div className='d-inline-block px-1' style={{ backgroundColor: "#00000022", color: '#FFFFFF66' }}>
                    PROJECT
                </div>
            </div>

            <div className='ms-2 mb-2'>
                <div><b>{this.state.title}</b></div>
                <div>{this.state.description}</div>
            </div>

            <div className='pb-2'>
                <button class="btn btn-primary btn-sm ms-2" type="button"
                    data-bs-toggle="collapse" data-bs-target="#collapseDetails"
                    aria-expanded="false" aria-controls="collapseExample">
                    Details
                </button>
                <button class="btn btn-primary btn-sm ms-2" type="button"
                    data-bs-toggle="collapse" data-bs-target="#collapseExperiments"
                    aria-expanded="false" aria-controls="collapseExample">
                    Experiments ({Object.entries(this.state.experiments).length})
                </button>
            </div>

            <div className="collapse m-2 px-3 py-2 rounded" id="collapseDetails" style={{ backgroundColor: '#FFFFFF11' }}>
                <div className='fs-5'>Path:</div>
                <div className='font-monospace' style={{ color: "rgb(255,122,208)", opacity: .7 }}>{this.state.path}</div>
                <div className='fs-5 mt-2'>Notes:</div>
                <div className='font-monospace' style={{ opacity: .5 }}>{this.state.notes}</div>
            </div>

            <div className="collapse m-2 px-3 py-2 rounded" id="collapseExperiments" style={{ backgroundColor: '#FFFFFF11' }}>
                <div className='fs-5'>ABF Experiments</div>
                <ul>
                    {Object.entries(this.state.experiments).map(([k, v]) => (
                        <li>{v.title} - <span className='text-muted'>{v.description}</span></li>)
                    )}
                </ul>
            </div>


        </div >
    }
}

export default ProjectHeader;