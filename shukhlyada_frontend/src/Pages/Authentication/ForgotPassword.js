import React,{useState} from "react";
import Button from "@material-ui/core/Button";
import CssBaseline from "@material-ui/core/CssBaseline";
import TextField from "@material-ui/core/TextField";
import Typography from "@material-ui/core/Typography";
import { makeStyles } from "@material-ui/core/styles";
import Container from "@material-ui/core/Container";
import { Link } from "react-router-dom";
import MuiAlert from '@material-ui/lab/Alert';
import Snackbar from '@material-ui/core/Snackbar';

function Alert(props) {
    return <MuiAlert elevation={6} variant="filled" {...props} />;
}

const useStyles = makeStyles((theme) => ({
    paper: {
        marginTop: theme.spacing(8),
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
    },
    form: {
        width: "100%", // Fix IE 11 issue.
        marginTop: theme.spacing(1),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
    snackbar: {
        marginTop: "50px"
    }
}));

export default function ForgotPassword() {
    const classes = useStyles();

    const redirectTime = "3500";
    function timedRedirect() {
        setTimeout("location.href = \"/\";",redirectTime);
    }

    const [state, setState] = React.useState({
        open: false,
        vertical: 'top',
        horizontal: 'center',
    });
    const handleClick = (newState) => () => {
        setState({ open: true, ...newState });
        timedRedirect();
    };

    const handleClose = () => {
        setState({ ...state, open: false });
    };
    const { vertical, horizontal, open } = state;


        return (
        <Container component="main" maxWidth="xs">
            <CssBaseline />
            <div className={classes.paper}>
                <Typography component="h1" variant="h5">
                    Forgot your password?
                </Typography>
                <Typography variant="subtitle2" gutterBottom>

                </Typography>
                <form className={classes.form} noValidate>
                    <TextField
                        variant="outlined"
                        margin="normal"
                        required
                        fullWidth
                        id="email"
                        label="Email Address"
                        name="email"
                        autoComplete="email"
                        autoFocus
                    />
                    <Button
                        onClick={handleClick({ vertical: 'top', horizontal: 'right' })}
                        fullWidth
                        variant="contained"
                        color="primary"
                        className={classes.submit}
                    >
                        Submit
                    </Button>
                    <Snackbar
                        className={classes.snackbar}
                        open={open} autoHideDuration={3000}
                        anchorOrigin={{ vertical, horizontal }}
                        onClose={handleClose}
                        message="I love snacks"
                        key={vertical + horizontal}
                    >
                        <Alert onClose={handleClose} severity="success">
                            Massage was sent on your email
                        </Alert>
                    </Snackbar>
                </form>
            </div>
        </Container>
    );
}
