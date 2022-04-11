# ABF Browser

**ABF Browser** is a web application for managing electrophysiology projects involving ABF (Axon Binary Format) files. It uses a ReactJS frontend to interact with a RESTful API provided by a PHP backend.

### Project Structure

```bash
# Large projects get a folder with notes, data, and analyses.
X:/Data/strain/project/
X:/Data/strain/project/design.ppt
X:/Data/strain/project/notebook.docx
X:/Data/strain/project/micrographs/ihc-20x.tif
X:/Data/strain/project/video/gcamp.avi
X:/Data/strain/project/2p/LineScan-123/frame-001.ch0.tif
X:/Data/strain/project/analysis/figures.opj

# E-phys projects have an 'abfs' folder for notes and data.
# Experiment file describes things that span days (experiment design).
# Daily experiment file describes things specific to that day (animal age).
X:/Data/strain/project/abfs/
X:/Data/strain/project/abfs/abfExperiment.json
X:/Data/strain/project/abfs/2022-12-24-DIC3/abfExperimentDay.json
X:/Data/strain/project/abfs/2022-12-24-DIC3/2022_12_24_0003.abf
X:/Data/strain/project/abfs/2022-12-24-DIC3/2022_12_24_0003.tif

# None of these folders are intended to be moved or separated from the rest.
# The project folder as a whole may be moved.
```

Is this different than what we do now? Mostly no.
* Project folders remain unchanged
* Insist on creating an `abfs` subfolder
* Insist on putting abfs in daily sub-folders
* `cells.txt` will no longer be edited by hand
  * groups/colors/notes are defined in daily `abfExperimentDay.json`
  * edits require the lab website
* `experiment.txt` will no longer be edited by hand
  * experiment notes spanning multiple days go in `abfExperimentDay.json`
  * edits require the lab website