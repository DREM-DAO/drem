import React, { useState } from 'react'
import Layout from '../components/Layout'
import styles from '../styles/Rec.module.css'

export interface IOpportuniuty {
  property: {
      assetDesc: string
      assetValue: number
      comments: string
      recName: string
      addressLine1: string
      addressLine2?: string
      city: string
      state: string
      country: string
      postalcode: string
      contactFirstName: string
      contactLastName: string
      contactMiddleName?: string
      contactPhoneNumber: string
      contactEmail: string
  }
}

function Opportunity() {

  const [input, setInput] = useState({assetDesc:"", assetValue:"", comments:"",recName:"",
          addressLine1: "", addressLine2: "", city: "",  state: "", country: "", postalcode: "",
          contactFirstName:"", contactLastName: "", contactMiddleName:"",
          contactPhoneNumber: "", contactEmail: "",
      }) 

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setInput({ ...input, [e.target.name]: e.target.value })
  }

const handleClick = () => {
    if(!input.assetDesc || !input.assetValue ) return

    //setInput({...input, assetDesc: input.assetDesc });

    console.log("Input: " ,  {...input} )

}

  return (
    <Layout>
    <div className={styles.AddRec}>
          <h1>Asset Identification </h1>
           <hr/>
           <div>
           Real Estate Company Name:&nbsp;
           <input type="textArea" onChange={handleChange} className={styles.RecInput}
                  name="recName" value={input.recName} placeholder="recName"  /> &nbsp;
           </div>
           <hr/>
          <div> 
            Asset Description:&nbsp;
            <input type="textArea" onChange={handleChange} className={styles.RecInput}
                  name="assetDesc" value={input.assetDesc} placeholder="assetDesc"  /> &nbsp;
            Asset Value:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="assetValue" value={input.assetValue}  placeholder="assetValue"  /> &nbsp;
            Comments:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput} 
                  name="comments" value={input.comments}  placeholder="comments"  />
          </div>

          <hr/>
          <p><b>Asset Address Information: </b></p>
          <div> 
            AddressLine1:&nbsp;
            <input type="text" onChange={handleChange} className={styles.RecInput}
                  name="addressLine1" value={input.addressLine1} placeholder="addressLine1"  /> &nbsp;
            AddressLine2:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="addressLine2" value={input.addressLine2}  placeholder="addressLine2"  /> &nbsp;
            City:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput} 
                  name="city" value={input.city}  placeholder="city"  />
          </div>
          <div> 
            State:&nbsp;
            <input type="text" onChange={handleChange} className={styles.RecInput}
                  name="state" value={input.state} placeholder="state"  /> &nbsp;
            Country:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="country" value={input.country}  placeholder="country"  /> &nbsp;
            Postalcode:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput} 
                  name="postalcode" value={input.postalcode}  placeholder="postalcode"  />
          </div>

          <hr/>
          <p><b>Asset Current Owner Information: </b></p>
         
          <div> 
            First Name:&nbsp;
            <input type="text" onChange={handleChange} className={styles.RecInput}
                  name="contactFirstName" value={input.contactFirstName} placeholder="contactFirstName"  /> &nbsp;
            Last Name:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="contactLastName" value={input.contactLastName}  placeholder="contactLastName"  /> &nbsp;
            Middle Name&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput} 
                  name="contactMiddleName" value={input.contactMiddleName}  placeholder="contactMiddleName"  />
          </div>
          
          <div> 
            <label htmlFor="Contact Phone Number">Phone Number: </label>  &nbsp;
            <input type="text" onChange={handleChange} className={styles.RecInput}
                  name="contactPhoneNumber" value={input.contactPhoneNumber} placeholder="contactPhoneNumber"  /> &nbsp;
            Email:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="contactEmail" value={input.contactEmail}  placeholder="contactEmail"  /> &nbsp;
          </div>

          <div className="line"></div>
        <br/>
        
        <button onClick={handleClick} className={styles.RecBtn} > Save </button>
     
    </div>
    </Layout>
  );
}

export default Opportunity;
