import React from "react";
import { Drawer } from "@material-ui/core";
import OutsideClick from "./outsideClick";
import "./style.scss";
import { CloseOutlined } from "@ant-design/icons";

const EditModel = (props) => {
  const { open, onClickOutside, children } = props;
  const handleClickOutsideModal = (event) => {
    if (!!onClickOutside) onClickOutside(event);
  };

  const onClose = () => {
    if (!!props.onClose) props.onClose();
  };

  function disableBodyScroll() {
    setTimeout(() => {
      let popups = document.getElementsByClassName(
        "MuiDrawer-paperAnchorBottom"
      );
      let disableScroll = false;
      for (let index = 0; index < popups.length; index++) {
        const c = popups[index];
        if (!c.style.visibility) {
          disableScroll = true;
        }
      }

      if (disableScroll) {
        document.body.classList.add("disable-body-scroll");
      } else {
        document.body.classList.remove("disable-body-scroll");
      }
    }, 500);
  }

  disableBodyScroll();

  return (
    <React.Fragment>
      <OutsideClick onClickOutside={handleClickOutsideModal}>
        <Drawer
          anchor="right"
          open={open}
          hidebackdrop={"false"}
          variant="persistent"
          className="plugin-config-model"
          title={props.title||"Titile"}
         
        >
          <div className="property-dialog" style={{width:500}}>
            <div className="header" style={{ textAlign: "right" }}>
              <a onClick={onClose}>
                <CloseOutlined />
              </a>
            </div>
            <div className="property-body">{children}</div>
          </div>
        </Drawer>
      </OutsideClick>
    </React.Fragment>
  );
};

export default EditModel;
