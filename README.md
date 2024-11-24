# Calibrated Photo Measure Application (Windows)

## Zweck

Diese Software soll die Vermessung der Größe eines Objekts auf einem Foto vereinfachen.
Mit Hilfe eines weiteren Objekts mit bekannter Abmessung wird das Foto zunächst zu Vermessung kalibriert.
Die anschliessende Vermessung des zu untersuchenden Objekts erlaubt beliebige Formen.

## Installation

Die Software kann herunter geladen werden und ist auf einem Windows-System lauffähig.
Der Download sollte in einen entsprechend benannten Ordner für die Software erfolgen.
In diesem Ordner sollte die auszuführnde Datei "" als Link auf den Desktop o.ä. kopiert werden, 
um einen einfachen Zugriff zu haben.


## Bedienungsanleitung

1. Laden des Bildes
![01_ImageLoaded](https://github.com/user-attachments/assets/19b93d0e-237f-4ef9-a1a9-bbe2c495fc38)

2. Eintragen des Nutzernamen und der bekannten Objektgröße in cm auf dem Foto. Auf dem Foto ist bekannt, dass der grüne Balken 7 cm breit ist.
![02_CalibrationClickStart](https://github.com/user-attachments/assets/97a30a7b-4b9b-4c13-abf3-25b399846994)

3. Der Start der Kalibrierung erfolgt durch klicken auf den Button "Eichungsmessung". Der Button färbt sich rot und dr Mauszeiger ändert sich zu einem Kreuz-Symbol.
 
4. Nun sollen zwei Punkte auf dem Fotogesetzt werden, die der bekannten Objektgröße entsprchen. Auf dem Foto wird dazu durch linken Mausklick der erste Punkt an der einen Balkenseite gesetzt und dann im senkrechten Abstand der zweite und letzte Punkt auf der anderen Balkenseite. Diese Entfernung entspricht in der Realität 7 cm. Zu optischen Unterstützung zum Finden der Senkrechten des Balkens wird eine dynamische Linie zwischen dem ersten Punkt und er Mausposition angezeigt (blaue Linie). Nach dem setzen des Endunkts ist die Kalibierung abgeschlossen und der Mauszeiger wird wieder "normal" und der Button grau.

![03_CalibrationFirstPoint](https://github.com/user-attachments/assets/8ea3ddbe-cb68-4848-909d-67ccc9482c96)
Ein kleiner roter Punkt wurde am linken Balkenrand gesetzt und der Mauszeiger befindet sich als Kreuz über dem rechten Balkenrand.
![04_CalibrationFirstPointWithDynamicLineBlue](https://github.com/user-attachments/assets/950a3a48-91c8-4ecf-93f7-a70073d21edb)
Nach dem Setzen des ersten Punkts zieht der Mauszeiger eine dynamische blaue Linie mit sich, die optisch helfen soll, den senkkrecht auf der gegenüberliegenden Balkenseite liegnden Punkt zu bestimmen.
![05_CalibrationEndPointReady](https://github.com/user-attachments/assets/82b60ec2-c2d7-4e2b-9e35-5ba3cb9c13fd)
Wird der zweite Punkt gesetzt, erscheint das Kalibrierungsmaß als rote Linie.

5. Nach der Kalibirierung beginnt die Vermessung. Dazu wird in der App der Button "Messung" geklickt. Der Buttung ist nun während der Messung grün gefärbt und der Mauszeiger wird zu einem Stift-Symbol.
![06_CalibrationLineAndMeasurePoints](https://github.com/user-attachments/assets/15d90a3f-e2fe-417b-88ec-89f9d4fba555)

7. Zum Vermessen werden auf dem Foto mit der Maus (linke Taste) grüne Punkte gesetzt, die beginnend am Kopf oder Schwanz entlang der Wirbelsäule entlang verlaufen.

![07_MeasurePointsEnd](https://github.com/user-attachments/assets/8e9cd606-09d9-483c-809c-db87b7b48982)

8. Ist der letzte Vermessungspunkt gesetzt, wird wieder auf den Button "Messung" geklickt, um die Messung abzuschließen. Die Punktreihe wird in einen Spline umgewandelt, um die Körperform besser nachzubilden.
![08_CalibrationLineAndMeasuredSpline](https://github.com/user-attachments/assets/2fe13101-6623-417a-8b3f-892c749ba8b7)

Unterhalb des "Messung"-Buttons erscheint die gemessenen Länge in cm.

9. Ist das Ergebnis zufriedenstellend, kann die Messung über "Messung speichern" als xlsx- und json-Datei gespeichert werden.

![09_CalibrationLineAndMeasuredSplineAndSaved](https://github.com/user-attachments/assets/84b200de-7c66-47e3-8e08-9eefb9fc4d0e)

10. Ist das Ergebnis nicht gut und die Messung soll wiederholt werden, werden alle Messdaten mit Reset bereinigt. Das Foto muss neu geladen werden.
13. Ist das Ergebnis gut, kann nach dem "Reset" ein neues Foto vermessen werden.


## Konfiguration

Der Name des Nutzers und die Grösse des bekannten Objekts kann als Konfiguration übernommen werden.
Damit stehen diese Angaben beim Starten des Programms sofort zur Verfügung.

