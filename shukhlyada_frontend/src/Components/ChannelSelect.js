import React, { useState } from "react";
import { fade, makeStyles } from "@material-ui/core/styles";
import Select from "@material-ui/core/Select";
import FormControl from "@material-ui/core/FormControl";
import MenuItem from "@material-ui/core/MenuItem";

const useStyles = makeStyles((theme) => ({
    title: {
        flexGrow: 50, // ця залупа відповідає за сторону розміщення пошукового поля(Якщо щось піде не так то видалити її нахуй)
        display: "none",
        [theme.breakpoints.up("sm")]: {
          display: "block",
          color: "white",
        },
    },
    
  
}));

export default function PrimarySearchAppBar() {
  const classes = useStyles();

  const [val, setVal] = useState(0);

  const handleChange = (event) => {
    setVal(event.target.value);
  };

  return (
    <FormControl>
      <Select
        className={classes.title}
        disableUnderline
        onChange={handleChange}
        value={val}
      >
        <MenuItem value={0}>Шухляда</MenuItem>
        <MenuItem value={1}>Sketch</MenuItem>
      </Select>
    </FormControl>
  );
}
