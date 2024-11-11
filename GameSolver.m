% Initialize Python and import pyautogui
if count(py.sys.path,'') == 0
    insert(py.sys.path,int32(0),'');
end
py.importlib.import_module('pyautogui');

pause(3);
% Start the game by pressing the Start button
startButton_location = py.pyautogui.locateOnScreen('UI_Elements/StartButton.png', confidence = 0.8);
py.pyautogui.moveTo(startButton_location.left + 1, startButton_location.top + 1);
py.pyautogui.click();

% Find the UI game elements
centerOfTheScreen_location = py.pyautogui.locateOnScreen('UI_Elements/CenterOfTheScreen.png');
redDot_location = py.pyautogui.locateOnScreen('UI_Elements/RedDot.png', confidence = 0.8);
targetArea_location = py.pyautogui.locateOnScreen('UI_Elements/TargetArea.png', confidence = 0.8);
absoulteCenterX = 1920 / 2;
absoulteCenterY = 1080 / 2;

%py.pyautogui.moveTo(absoulteCenterX, absoulteCenterY);
% pause(10);
% py.pyautogui.moveTo(absoulteCenterX + ((targetArea_location.left + 25) - (centerOfTheScreen_location.left + 6)) * 2.49027, absoulteCenterY + ((targetArea_location.top + 25) - (centerOfTheScreen_location.top + 6)) * 2.3427);
py.pyautogui.moveTo(absoulteCenterX + ((targetArea_location.left + 25) - (centerOfTheScreen_location.left + 6)) * (1920 / 771), absoulteCenterY + ((targetArea_location.top + 25) - (centerOfTheScreen_location.top + 6)) * (1080 / 461));

pause(10);
% Stop the game by pressing the Stop button
stopButton_location = py.pyautogui.locateOnScreen('UI_Elements/StopButton.png', confidence = 0.8);
py.pyautogui.moveTo(stopButton_location.left + 1, stopButton_location.top + 1);
py.pyautogui.click();
