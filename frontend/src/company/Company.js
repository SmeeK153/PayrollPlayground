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
import NewEmployeeDialog from "./NewEmployeeDialog"

import {
    Link,
    useParams
} from "react-router-dom";

const useStyles = makeStyles({
    table: {
        minWidth: 650,
    },
});

export default function Company() {
    const [employees, setEmployees] = useState([]);
    let {company} = useParams();
    useEffect(() => {
        LoadEmployees();
    });

    const LoadEmployees = async () => {
        const response = await Client.get(`v1/companies/${company}`);
        const data = response.data;
        if (!!data){
            setEmployees(data.employees);
        }
        else {
            setEmployees([]);
        }
    }

    const AddEmployee = async (taxId, firstName, lastName) => {
        await Client.post(`v1/companies/${company}/employees`,{
            taxIdentificationNumber: taxId,
            employeeFirstName: firstName,
            employeeLastName: lastName
        });
        LoadEmployees();
    }

    const classes = useStyles();
    return (
        <TableContainer component={Paper}>
            <Table className={classes.table} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>
                            <h1>
                                Employees
                            </h1>
                            <NewEmployeeDialog
                                handleCreate={AddEmployee}
                            />
                        </TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {employees.map((row) => (
                        <TableRow key={row.id}>
                            <TableCell component="th" scope="row" >
                                <Link to={`/company/${company}/employees/${row.id}`}>
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
