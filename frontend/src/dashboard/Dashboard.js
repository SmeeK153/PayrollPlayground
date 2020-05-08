import React, {useState, useEffect} from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import Client from '../api/Client';
import NewCompanyDialog from "./NewCompanyDialog";
import {
    Link
} from "react-router-dom";

const useStyles = makeStyles({
    table: {
        minWidth: 650,
    },
});

export default function Dashboard() {
    const [companies, setCompanies] = useState([]);

    useEffect(() => {
        LoadCompanies();
    }, []);

    const LoadCompanies = async () => {
        const response = await Client.get('v1/companies/');
        const data = response.data;
        if (!!data){
            setCompanies(data);
        }
        else {
            setCompanies([]);
        }
    }

    const AddCompany = async (name) => {
        await Client.post('v1/companies',{
            name: name,
            paychecksPerYear: 26
        });
        LoadCompanies();
    }

    const classes = useStyles();
    return (
        <TableContainer component={Paper}>
            <Table className={classes.table} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>
                            <h1>
                                Companies
                            </h1>
                            <NewCompanyDialog
                                handleCreate={AddCompany}
                            />
                        </TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {companies.map((row) => (
                        <TableRow key={row.id}>
                                <TableCell component="th" scope="row" >
                                    <Link to={`/company/${row.id}`}>
                                        {row.name}
                                    </Link>
                                </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}
