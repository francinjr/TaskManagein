import { useEffect } from "react";
import styles from "./styles.module.css";

interface ApiResponseAlertProps {
  operationStatus: boolean;
  message: string;
  messageDuration: number;
  showApiResponseAlert: boolean;
  setShowApiResponseAlert: (showApiResponseAlert: boolean) => void;
}

export function ApiResponseAlert(props: ApiResponseAlertProps) {
  //const [visible, setVisible] = useState(false);

  useEffect(() => {
    if (props.showApiResponseAlert) {
      const timer = setTimeout(() => {
        props.setShowApiResponseAlert(false);
      }, props.messageDuration);

      return () => clearTimeout(timer);
    }
  }, [props.message, props.messageDuration]);

  if (!props.showApiResponseAlert) return null;

  return (
    <div className={styles.alertContainer}>
      {props.message}
    </div>
  );
}
