# Calibrated Photo Measure Application (Windows)

## Zweck

Diese Software dient der Vermessung der Größe eines Objekts auf einem Foto. 
Mit Hilfe eines weiteren Objekts mit bekannter Abmessung wird das Foto zunächst für die Vermessung kalibriert.
Die anschliessende Vermessung des zu untersuchenden Objekts erlaubt beliebige Formen: Das Objekt wird als Linie digitalisiert und vermessen, z.B. als sich schlängelte Linie vom Kopf entlang des Rückgrats bis zum Schwanzende eines Salamanders.

## Installation

Die lauffähige Software kann hier herunter geladen werden. Sie ist auf einem Windows-System lauffähig (z.B Win11, 64-bit).
![Install_01_Releases](https://github.com/user-attachments/assets/ec42cfb1-2bf0-477b-a12c-f200b1218120)
![Install_02_tag](https://github.com/user-attachments/assets/185b521c-8c6d-4ba6-8c06-24df4bfe3ba1)
![Install_03_zip_and_exe](https://github.com/user-attachments/assets/a8967524-44fb-4682-b12f-5488830028ca)

Der Download sollte in einen entsprechend benannten Ordner erfolgen. Die auszuführende Datei "PhotoMeasureCalibrated.exe" kann mit "Verknüpfung hier erstellem" als Link auf den Desktop o.ä. kopiert werden, 
um einen einfachen Zugriff zu erhalten.


## Bedienungsanleitung

Nach dem Starten des Programms sollte es bildschirmfüllend gestellt werden. Dann kann das erste Foto geladen werden.
![Start_Fullscreeen](https://github.com/user-attachments/assets/a5b4ef16-0889-47f9-ae47-41dd13fab782)

1. Laden des Bildes
![01_ImageLoaded](https://github.com/user-attachments/assets/19b93d0e-237f-4ef9-a1a9-bbe2c495fc38)

Vorab der Vermessung einer Serie von Fotos aus dem gleichen Umfeld sollte vorab der Benutzername und die bekannte Grösse des Objekts mit bekannter Abmessung eingetragen werden. Diese Angaben bleiben bei allen folgenden Messungen erhalten.

2. Eintragen des Nutzernamen und der bekannten Objektgröße in cm auf dem Foto. Auf dem Foto ist z.B. bekannt, dass der grüne Balken 7 cm breit ist.

![02_CalibrationClickStart](https://github.com/user-attachments/assets/97a30a7b-4b9b-4c13-abf3-25b399846994)

3. Der Start der Kalibrierungsmessung erfolgt durch Klicken (linker Mausknopf) auf den Button "Eichungsmessung". Der Button färbt sich daraufhin rot und der Mauszeiger wird sich zu einem Fadenkreuz (Kreuz-Symbol).
 
Nun sollen zwei Punkte auf dem Foto gesetzt werden, die der bekannten Objektgröße entsprechen: Hier der 7 cm Breite des Balkens.
4. Auf dem Foto wird dazu durch linken Mausklick der erste Punkt an der einen Balkenseite gesetzt und dann im senkrechten Abstand der zweite und abschliessende Punkt auf der anderen Balkenseite. Diese Entfernung entspricht in der Realität 7 cm. Zur optischen Unterstützung des Finden der Senkrechten des Balkens wird eine dynamische Linie zwischen dem ersten Punkt und er Mausposition angezeigt (blaue Linie). 

Ein kleiner roter Punkt wurde am linken Balkenrand gesetzt und der Mauszeiger befindet sich als Kreuz über dem rechten Balkenrand.
![04_CalibrationFirstPointWithDynamicLineBlue](https://github.com/user-attachments/assets/6831332f-7539-4676-9990-96e36404b0ba)

Nach dem Setzen des ersten Punkts zieht der Mauszeiger eine dynamische blaue Linie mit sich, die optisch helfen soll, den senkkrecht auf der gegenüberliegenden Balkenseite liegnden Punkt zu bestimmen.
![05_CalibrationEndPointReady](https://github.com/user-attachments/assets/82b60ec2-c2d7-4e2b-9e35-5ba3cb9c13fd)
Nach dem Setzen des Endpunkts ist die Kalibierung abgeschlossen und der Mauszeiger wird wieder "normal" und der Button grau. Die Farbe der beweglichen Linie ändert sich von blau zu rot und entspricht der bekannten Distanz.

Nach der Kalibirierung beginnt die Vermessung.

5. Dazu wird in der App der Button "Messung" geklickt. Der Button ist während der Messung grün gefärbt und der Mauszeiger wird zu einem Stift-Symbol.
Zum Vermessen werden auf dem Foto mit der Maus (linke Taste) grüne Punkte gesetzt, die beginnend am Kopf oder Schwanz entlang der Wirbelsäule entlang verlaufen.
![06_CalibrationLineAndMeasurePoints](https://github.com/user-attachments/assets/15d90a3f-e2fe-417b-88ec-89f9d4fba555)
Setzen der Vermessungspunkte von Kopf bis Schwanzende..
![07_MeasurePointsEnd](https://github.com/user-attachments/assets/255cd53b-9d89-4ce5-a61b-b54a38f293ee)



7. Ist der letzte Vermessungspunkt gesetzt, wird wieder auf den Button "Messung" geklickt, um die Messung abzuschließen. Die Punktreihe wird in einen Spline umgewandelt, um die Körperform besser nachzubilden.
![08_CalibrationLineAndMeasuredSpline](https://github.com/user-attachments/assets/3a7c94bd-cd1f-4849-b99c-b275b81adf01)
Unterhalb des "Messung"-Buttons erscheint die gemessenen Länge in cm.

8. Ist das Ergebnis zufriedenstellend, kann die Messung über "Messung speichern" als xlsx- und json-Datei gespeichert werden. Nach dem Speichern färbt sich das Rechteck unter dem Button grün.

![09_CalibrationLineAndMeasuredSplineAndSaved](https://github.com/user-attachments/assets/84b200de-7c66-47e3-8e08-9eefb9fc4d0e)

9. Ist das Ergebnis nicht gut und die Messung soll wiederholt werden, werden alle Messdaten mit dem Button "Reset" entfernt. Das Foto muss neu geladen werden.
10. Ist das MessErgebnis gut, kann ein neues Foto vermessen werden. Dazu ist ebenfalls der Button "Reset" zu drücken, um das vorherige Foto mit den Messdaten zu löschen.


## Konfiguration

Der Name des Nutzers und die Grösse des bekannten Objekts werden als fixe Werte übernommen. Sie stehen damit stehen nach Starten des Programms sofort zur Verfügung und müssen nicht wiederholt für jede Messung eingegeben werden.

## Speicherung der Messergebnisse

Die Messergebnisse werden in einer Excel- (.xslx) und in einer json-Datei (.json) gesichert. Der Dateiname entspricht dem Namen der Fotodatei mit Zusatz "calibratedMeasure".

![10_Excel_Nr152_20151231_ganz_calibratedMeasure](https://github.com/user-attachments/assets/efaafc42-8789-42d6-bacf-10aabacf1cfb)

![11_JSON_Clip_Nr152_20151231_ganz_calibratedMeasure](https://github.com/user-attachments/assets/c5dcc59c-e2e4-41a9-a4df-ce2cede05e65)




