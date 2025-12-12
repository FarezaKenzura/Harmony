using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private WaveType _waveType = WaveType.Sin;
    [SerializeField] private float _width = 20f;
    [SerializeField] private float _amplitude = 1f;
    [SerializeField] private float _frequency = 1f;
    [SerializeField] private float _speedMultiplier = 1f;
    [SerializeField] private int pointCount = 100;
    private List<Vector3> _linePoints = new List<Vector3>();

    private float ActualSpeed => _frequency * _speedMultiplier;
    private float CurrentTime => Time.timeSinceLevelLoad;

    private void Update()
    {
        DrawWave();
    }

    public void DrawWave()
    {
        var xStart = -_width / 2f + transform.position.x;
        var xEnd = _width / 2f + transform.position.x;
        var yDiff = transform.position.y;

        switch (_waveType)
        {
            case WaveType.Sin:
                DrawSinWave(xStart, xEnd, yDiff);
                break;
            case WaveType.Square:
                DrawSquareWave(xStart, xEnd, yDiff);
                break;
            case WaveType.Triangle:
                DrawTriangleWave(xStart, xEnd, yDiff);
                break;
            case WaveType.Sawtooth:
                DrawSawtoothWave(xStart, xEnd, yDiff);
                break;
            case WaveType.Pulse:
                DrawPulseWave(xStart, xEnd, yDiff);
                break;
            default:
                _lineRenderer.positionCount = 0;
                break;
        }
    }

    private void DrawSinWave(float xStart, float xEnd, float yDiff)
    {
        _lineRenderer.positionCount = pointCount;

        for (var currentPoint = 0; currentPoint < pointCount; currentPoint++)
        {
            var progress = (float)currentPoint / (pointCount - 1);
            var x = Mathf.Lerp(xStart, xEnd, progress);
            var y = yDiff + WaveHandler.GetWaveY(WaveType.Sin, x, _amplitude, _frequency, ActualSpeed, CurrentTime);

            _lineRenderer.SetPosition(currentPoint, new Vector3(x, y, 0f));
        }
    }

    private void DrawSquareWave(float xStart, float xEnd, float yDiff)
    {
        _linePoints.Clear();
        var time = CurrentTime;
        var actualSpeed = ActualSpeed;

        _linePoints.Add(new Vector3(xStart, yDiff + WaveHandler.GetWaveY(WaveType.Square, xStart, _amplitude, _frequency, actualSpeed, time), 0f));

        var phase = actualSpeed * time;
        var firstTransition = Mathf.Ceil((_frequency * xStart + phase) / Mathf.PI);
        var numTransitions = Mathf.CeilToInt((_frequency * xEnd + phase) / Mathf.PI) - firstTransition + 1;

        for (int i = 0; i < numTransitions; i++)
        {
            var n = firstTransition + i;
            var transitionX = (n * Mathf.PI - phase) / _frequency;

            if (transitionX > xStart && transitionX < xEnd)
            {
                var currentY = yDiff + WaveHandler.GetWaveY(WaveType.Square, transitionX - 0.001f, _amplitude, _frequency, actualSpeed, time);
                _linePoints.Add(new Vector3(transitionX, currentY, 0f));

                var newY = yDiff + WaveHandler.GetWaveY(WaveType.Square, transitionX + 0.001f, _amplitude, _frequency, actualSpeed, time);
                _linePoints.Add(new Vector3(transitionX, newY, 0f));
            }
        }

        _linePoints.Add(new Vector3(xEnd, yDiff + WaveHandler.GetWaveY(WaveType.Square, xEnd, _amplitude, _frequency, actualSpeed, time), 0f));

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
    }

    private void DrawTriangleWave(float xStart, float xEnd, float yDiff)
    {
        _linePoints.Clear();
        var time = CurrentTime;
        var actualSpeed = ActualSpeed;

        _linePoints.Add(new Vector3(xStart, yDiff + WaveHandler.GetWaveY(WaveType.Triangle, xStart, _amplitude, _frequency, actualSpeed, time), 0f));

        var phase = actualSpeed * time;
        var firstPeak = Mathf.Ceil((_frequency * xStart + phase) / Mathf.PI);
        var numPeaks = Mathf.CeilToInt((_frequency * xEnd + phase) / Mathf.PI) - firstPeak + 1;

        for (int i = 0; i < numPeaks; i++)
        {
            var n = firstPeak + i;
            var peakX = (n * Mathf.PI - phase) / _frequency;

            if (peakX > xStart && peakX < xEnd)
            {
                var y = yDiff + WaveHandler.GetWaveY(WaveType.Triangle, peakX, _amplitude, _frequency, actualSpeed, time);
                _linePoints.Add(new Vector3(peakX, y, 0f));
            }
        }

        _linePoints.Add(new Vector3(xEnd, yDiff + WaveHandler.GetWaveY(WaveType.Triangle, xEnd, _amplitude, _frequency, actualSpeed, time), 0f));

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
    }

    private void DrawSawtoothWave(float xStart, float xEnd, float yDiff)
    {
        _linePoints.Clear();
        var time = CurrentTime;
        var actualSpeed = ActualSpeed;

        _linePoints.Add(new Vector3(xStart, yDiff + WaveHandler.GetWaveY(WaveType.Sawtooth, xStart, _amplitude, _frequency, actualSpeed, time), 0f));

        var phase = actualSpeed * time;
        var period = 2f * Mathf.PI;

        var firstReset = Mathf.Ceil((_frequency * xStart + phase) / period);
        var numResets = Mathf.CeilToInt((_frequency * xEnd + phase) / period) - firstReset + 1;

        for (int i = 0; i < numResets; i++)
        {
            var n = firstReset + i;
            var resetX = (n * period - phase) / _frequency;

            if (resetX > xStart && resetX < xEnd)
            {
                var currentY = yDiff + WaveHandler.GetWaveY(WaveType.Sawtooth, resetX - 0.001f, _amplitude, _frequency, actualSpeed, time);
                _linePoints.Add(new Vector3(resetX, currentY, 0f));

                var newY = yDiff + WaveHandler.GetWaveY(WaveType.Sawtooth, resetX + 0.001f, _amplitude, _frequency, actualSpeed, time);
                _linePoints.Add(new Vector3(resetX, newY, 0f));
            }
        }

        _linePoints.Add(new Vector3(xEnd, yDiff + WaveHandler.GetWaveY(WaveType.Sawtooth, xEnd, _amplitude, _frequency, actualSpeed, time), 0f));

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
    }

    private void DrawPulseWave(float xStart, float xEnd, float yDiff)
    {
        const float dutyCycle = 0.2f;

        _linePoints.Clear();
        var time = CurrentTime;
        var actualSpeed = ActualSpeed;

        _linePoints.Add(new Vector3(xStart, yDiff + WaveHandler.GetWaveY(WaveType.Pulse, xStart, _amplitude, _frequency, actualSpeed, time), 0f));

        var phase = actualSpeed * time;
        var period = 2f * Mathf.PI;

        float xStartNorm = (_frequency * xStart + phase) / period;
        float xEndNorm = (_frequency * xEnd + phase) / period;

        var firstT1 = Mathf.Ceil(xStartNorm - dutyCycle);
        var numT1 = Mathf.Floor(xEndNorm - dutyCycle) - firstT1 + 1;

        var firstT2 = Mathf.Ceil(xStartNorm);
        var numT2 = Mathf.Floor(xEndNorm) - firstT2 + 1;

        var allTransitions = new List<float>();

        for (int i = 0; i < numT1; i++)
        {
            var n = firstT1 + i;
            float transitionNorm = n + dutyCycle;
            float transitionX = (transitionNorm * period - phase) / _frequency;
            if (transitionX > xStart && transitionX < xEnd) allTransitions.Add(transitionX);
        }

        for (int i = 0; i < numT2; i++)
        {
            var n = firstT2 + i;
            float transitionNorm = n + 1f;
            float transitionX = (transitionNorm * period - phase) / _frequency;
            if (transitionX > xStart && transitionX < xEnd) allTransitions.Add(transitionX);
        }

        allTransitions.Sort();

        foreach (float transitionX in allTransitions)
        {
            var currentY = yDiff + WaveHandler.GetWaveY(WaveType.Pulse, transitionX - 0.001f, _amplitude, _frequency, actualSpeed, time);
            _linePoints.Add(new Vector3(transitionX, currentY, 0f));

            var newY = yDiff + WaveHandler.GetWaveY(WaveType.Pulse, transitionX + 0.001f, _amplitude, _frequency, actualSpeed, time);
            _linePoints.Add(new Vector3(transitionX, newY, 0f));
        }

        _linePoints.Add(new Vector3(xEnd, yDiff + WaveHandler.GetWaveY(WaveType.Pulse, xEnd, _amplitude, _frequency, actualSpeed, time), 0f));

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
    }
}
