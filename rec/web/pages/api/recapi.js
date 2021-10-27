import axios from "axios"; // axios requests


module.exports = async (req, res) => {
  console.log("call twiter api from local---", req.name)

//   const config = {
//     method: 'post',
//     url: `http://localhost:8080/REC/register`,
//     body: JSON.stringify({name: req.name})
//     headers: { 
//       'Content-Type': 'application/json',
//     }
//   }
//   let _res = await axios(config)
//console.log(_res.data);
//res.status(200).json(_res.data);

res.status(200).json("Success");
};
