import React, {useState, useEffect} from 'react';
import {makeStyles} from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import Client from '../api/Client';
import NewDependentDialog from "./NewDependentDialog";
import Grid from "@material-ui/core/Grid";

import {
    useParams
} from "react-router-dom";

const useStyles = makeStyles({
    table: {
        minWidth: 650,
    },
    report: {
        minWidth: 200
    }
});

export default function Company() {
    const [dependents, setDependents] = useState([]);
    const [lineItems, setLineItems] = useState([]);
    const [netPay, setNetPay] = useState(null);
    let {company, employee} = useParams();
    const formatter = new Intl.NumberFormat('en-US',{
        style:'currency',
        currency:'USD'
    });
    useEffect(() => {
        if (netPay === null){
            LoadEmployeeData();
        }
    });

    const LoadEmployeeData = async () => {
        const response = await Client.get(`v1/companies/${company}/employees/${employee}`);
        const data = response.data;
        if (!!data) {
            setDependents(data.dependents);
            setNetPay(data.netPay);
            var items = [
                {
                    description: "Employee Salary",
                    adjustment: data.salary
                }
            ]
            Object.keys(data.deductions).forEach(key => {
                items.push({
                    description: key,
                    adjustment: data.deductions[key] * -1
                });
            })
            setLineItems(items);
        } else {
            setDependents([]);
            setLineItems({});
        }
    }

    const AddDependent = async (taxId, firstName, lastName) => {
        await Client.post(`v1/companies/${company}/employees/${employee}/dependents`, {
            taxIdentificationNumber: taxId,
            dependentFirstName: firstName,
            dependentLastName: lastName
        });
        LoadEmployeeData();
    }

    const classes = useStyles();
    return (
        <Grid container spacing={3}>
            <Grid item xs={9}>
                <TableContainer component={Paper}>
                    <Table className={classes.table} aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <TableCell>
                                    <h1>
                                        Dependents
                                    </h1>
                                    <NewDependentDialog
                                        handleCreate={AddDependent}
                                    />
                                </TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {dependents.map((row) => (
                                <TableRow key={row.id}>
                                    <TableCell component="th" scope="row">
                                        {row.name}
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Grid>
            <Grid item xs={3}>
                <TableContainer component={Paper}>
                    <Table className={classes.report} aria-label="spanning table">
                        <TableHead>
                            <TableRow>
                                <TableCell align="left" colSpan={3}>
                                    <b>Line Item</b>
                                </TableCell>
                                <TableCell colSpan={1}></TableCell>
                                <TableCell align="right"><b>Adjustment</b></TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell><u>Description</u></TableCell>
                                <TableCell colSpan={3}></TableCell>
                                <TableCell align="right"><u>Total</u></TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {lineItems.map((row, i) => (
                                <TableRow key={i}>
                                    <TableCell><i>{row.description}</i></TableCell>
                                    <TableCell colSpan={3}></TableCell>
                                    <TableCell align="right">{formatter.format(row.adjustment/100)}</TableCell>
                                </TableRow>
                            ))}
                            <TableRow>
                                <TableCell colSpan={2}></TableCell>
                                <TableCell colSpan={2}>Annual Net Income</TableCell>
                                <TableCell align="right">{formatter.format(netPay/100)}</TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
            </Grid>
        </Grid>
    );
}
