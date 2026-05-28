# CybersecurityChatBot1599 (E-Bot)

## Overview
The Cybersecurity Awareness Bot (E-Bot) is a high-fidelity, modern desktop chat application developed in C# using Windows Presentation Foundation (WPF) and .NET 8. Migrated from a legacy text-based console framework, this intelligent security assistant is designed to train and evaluate users on critical online defense vectors through real-time communication, utility command parsing, and live threat simulations.

E-Bot combines responsive graphic styling with an underlying state machine engine to create an educational, secure training hub.

## Key Features
* **Modern Dark-Mode UI:** A premium, custom-styled graphical chat interface featuring distinct message bubbles (royal purple for users, sleek matte gray for the bot) alongside an administrative historical navigation sidebar.
* **Decoupled Retro ASCII Art:** Retains the classic console heritage banner extracted from `UIHelper.cs`, dynamically aligned and rendered inside the GUI layout using crisp monospace text scaling.
* **Native Audio Engine:** Integrates native, asynchronous playback of a local `greeting.wav` file upon application startup without freezing or introducing latency into the UI render pipeline.
* **Interactive Live-Fire Simulation Matrix:** Implements an advanced, multi-tier state machine that transitions users away from basic chatting into live cybersecurity scenarios (e.g., automated phishing email evaluations and physical corporate USB drops).
* **Persistent Performance Scorecard:** Telemetry tracking engine that logs user answers, tallies passed/failed simulation nodes, updates total search inquiries, and outputs a real-time security readiness rating profile.
* **Terminal Slash Command System:** Intercepts system commands (such as `/help`, `/status`, and `/topics`) to query database matrices and display diagnostic panels directly inside the chat environment.

## Cybersecurity Infrastructure Covered
Users can interact directly with the knowledge database or explore simulations regarding:
* **Password Architecture:** Enforcement of passphrases over single words, token hygiene, and credential reuse vulnerability data.
* **Phishing Audits:** Recognition of urgent terminology, suspicious email link domains, and corporate social engineering tactics.
* **Financial Fraud & Scams:** Identifying illegitimate tech support operations, crypto giveaways, and gift card extortion tricks.

## System Commands Terminal Menu
The chatbot automatically intercepts core administrative instructions starting with a forward slash (`/`):
* `SIMULATE` / `TEST` - Disrupts the idle loop and launches the interactive defensive scenario mode.
* `/help` - Displays the available navigation commands and utility keyword directives.
* `/status` - Prints the user's real-time Agent Security Scorecard, displaying past milestones, inquiries count, and current defensive tier.
* `/topics` - Outputs all security knowledge interfaces natively mapped within E-Bot's memory arrays.

## Technologies Used
* **Languages:** C#
* **Frameworks:** .NET 8.0, Windows Presentation Foundation (WPF)
* **Development Environment:** Visual Studio 2022
* **Version Control & DevSecOps:** Git, GitHub, Automated GitHub Actions CI/CD Build Engine Pipeline

---

## Visual Presentation & Interface Evolution

### Part 1: Initial Console Prototype Baseline
The historical command-line execution setup, displaying baseline layout logic validation before migrating to modern desktop containers:

![Console Chat Screenshot Placeholder](https://github.com/user-attachments/assets/04803552-4838-48af-a783-8d3e3ae7d9af)

<img width="1771" height="1033" alt="Legacy Input Processing" src="https://github.com/user-attachments/assets/d582f1b1-2fa2-4e98-b9b5-69e12dbcfda1" />

### Part 2: Upgraded High-Fidelity WPF GUI Layout
The newly completed graphical environment showcasing interactive text bubbles, the legacy ASCII title rendering cleanly via `UIHelper`, and live responses from the performance scorecard matrices:

<img width="1771" height="1033" alt="Legacy Input Processing" src="C:\Users\Student\Pictures\Screenshots\Screenshot 2026-05-28 151130.png" />
