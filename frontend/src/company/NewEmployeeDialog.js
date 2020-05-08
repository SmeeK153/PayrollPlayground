import React, {useState} from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';

export default function NewCompanyDialog(props) {
    const {handleCreate} = props;
    const [open, setOpen] = useState(false);
    const [taxId, setTaxId] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleSubmit = async () => {
        if(!!handleCreate){
            await handleCreate(taxId, firstName, lastName);
        }
        handleClose();
    }

    const handleTaxIdFieldChange = (e) => {
        e.preventDefault();
        setTaxId(e.target.value);
    }

    const handleFirstNameFieldChange = (e) => {
        e.preventDefault();
        setFirstName(e.target.value);
    }

    const handleLastNameFieldChange = (e) => {
        e.preventDefault();
        setLastName(e.target.value);
    }

    return (
        <div>
            <Button variant="outlined" color="primary" onClick={handleClickOpen}>
                Add Employee
            </Button>
            <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
                <DialogTitle id="form-dialog-title">Add New Employee</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Please enter the name and tax id of the new employee. By default, their bi-weekly salary will be $2,000
                        and they will be automatically enrolled into the company benefits program at a base cost of $1,000 annually.
                    </DialogContentText>
                    <TextField
                        autoFocus
                        margin="dense"
                        id="name"
                        label="Tax Identification Number"
                        type="text"
                        fullWidth
                        onChange={handleTaxIdFieldChange}
                    />
                    <TextField
                        margin="dense"
                        id="name"
                        label="First Name"
                        type="text"
                        fullWidth
                        onChange={handleFirstNameFieldChange}
                    />
                    <TextField
                        margin="dense"
                        id="name"
                        label="Last Name"
                        type="text"
                        fullWidth
                        onChange={handleLastNameFieldChange}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} color="primary">
                        Cancel
                    </Button>
                    <Button onClick={handleSubmit} color="primary">
                        Create
                    </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}
