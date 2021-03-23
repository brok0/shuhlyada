import React from "react";
import Button from "@material-ui/core/Button";
import { makeStyles } from "@material-ui/core/styles";
import TextField from "@material-ui/core/TextField";
import Dialog from "@material-ui/core/Dialog";
import DialogActions from "@material-ui/core/DialogActions";
import DialogContent from "@material-ui/core/DialogContent";
import DialogTitle from "@material-ui/core/DialogTitle";
import Select from "@material-ui/core/Select";
import InputLabel from "@material-ui/core/InputLabel";
import MenuItem from "@material-ui/core/MenuItem";
import Divider from "@material-ui/core/Divider";
import MoreVertIcon from "@material-ui/icons/MoreVert";
import IconButton from "@material-ui/core/IconButton";
import Slide from "@material-ui/core/Slide";
import ReportIcon from "@material-ui/icons/Report";
import { Tooltip } from "@material-ui/core";


const Transition = React.forwardRef(function Transition(props, ref) {
  return <Slide direction="up" ref={ref} {...props} />;
});
const useStyles = makeStyles((theme) => ({
  selectEmpty: {
    margin: 20,
  },
  label: {
    marginTop: 10,
    color: "black",
    fontFamily: "italic",
  },
}));
export default function ReportDialog() {
  const [open, setOpen] = React.useState(false);
  const classes = useStyles();
  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };
  const handleChange = (event) => {};

  return (
    <div>
      <Tooltip title="Report" placement="right">
        <IconButton
          variant="outlined"
          color="secondary"
          onClick={handleClickOpen}
          aria-label="reportpost"
        >
          <ReportIcon />
        </IconButton>
      </Tooltip>
      <Dialog
        open={open}
        onClose={handleClose}
        TransitionComponent={Transition}
        keepMounted
        aria-labelledby="form-dialog-title"
      >
        <DialogTitle id="form-dialog-title">
          Whats wrong with this one?
        </DialogTitle>
        <Divider variant="middle"></Divider>
        <DialogContent>
          <InputLabel className={classes.label}>Select reason</InputLabel>
          <Select onChange={handleChange} fullWidth>
            <MenuItem value="">
              <em>None</em>
            </MenuItem>
            <MenuItem value={10}>Ten</MenuItem>
            <MenuItem value={20}>Twenty</MenuItem>
            <MenuItem value={30}>Thirty</MenuItem>
          </Select>
          <InputLabel className={classes.label}>
            Write Your details(optional)
          </InputLabel>
          <TextField id="name" label="Details.." type="text" fullWidth />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose} color="primary">
            Close
          </Button>
          <Button onClick={handleClose} color="primary">
            Submit
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
